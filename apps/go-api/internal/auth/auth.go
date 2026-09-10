// Package auth verifies Clerk session tokens and stores the authenticated
// user's ID in a context.
package auth

import (
	"context"
	"crypto/rsa"
	"errors"
	"fmt"
	"slices"

	"github.com/golang-jwt/jwt/v5"
)

// ClerkVerifier verifies RS256 session tokens issued by Clerk.
type ClerkVerifier struct {
	parser            *jwt.Parser
	key               *rsa.PublicKey
	authorizedParties []string
}

// NewClerkVerifier returns a ClerkVerifier for tokens issued by issuer and
// signed with the RSA public key in pemKey.
//
// If authorizedParties is not empty, a token's azp claim, when present, must
// be one of them. Clerk sets azp to the origin of the page that requested the
// token, so this rejects tokens obtained by other sites.
func NewClerkVerifier(pemKey, issuer string, authorizedParties []string) (*ClerkVerifier, error) {
	key, err := jwt.ParseRSAPublicKeyFromPEM([]byte(pemKey))
	if err != nil {
		return nil, fmt.Errorf("parse clerk public key: %w", err)
	}
	return &ClerkVerifier{
		key:               key,
		authorizedParties: authorizedParties,
		parser: jwt.NewParser(
			jwt.WithValidMethods([]string{jwt.SigningMethodRS256.Alg()}),
			jwt.WithIssuer(issuer),
			jwt.WithExpirationRequired(),
			jwt.WithLeeway(0),
		),
	}, nil
}

// clerkClaims are the claims read from a Clerk session token.
type clerkClaims struct {
	jwt.RegisteredClaims
	AuthorizedParty string `json:"azp,omitempty"`
}

// Verify checks the token's signature, algorithm, issuer, expiry, not-before
// time and authorized party, and returns its subject, which is the Clerk user
// ID.
func (v *ClerkVerifier) Verify(token string) (string, error) {
	var claims clerkClaims
	if _, err := v.parser.ParseWithClaims(token, &claims, v.keyFunc); err != nil {
		// %v rather than %w: golang-jwt's error types are not part of this
		// package's API.
		return "", fmt.Errorf("invalid token: %v", err)
	}
	if claims.Subject == "" {
		return "", errors.New("invalid token: no subject")
	}
	if claims.AuthorizedParty != "" && len(v.authorizedParties) > 0 &&
		!slices.Contains(v.authorizedParties, claims.AuthorizedParty) {
		return "", fmt.Errorf("invalid token: unauthorized party %q", claims.AuthorizedParty)
	}
	return claims.Subject, nil
}

func (v *ClerkVerifier) keyFunc(*jwt.Token) (any, error) {
	return v.key, nil
}

// userKey is the context key for the user ID. It is unexported, so only this
// package can set or read the value.
type userKey struct{}

// WithUser returns a copy of ctx that carries userID.
func WithUser(ctx context.Context, userID string) context.Context {
	return context.WithValue(ctx, userKey{}, userID)
}

// UserFromContext returns the user ID stored in ctx by WithUser, and whether
// there was one.
func UserFromContext(ctx context.Context) (string, bool) {
	userID, ok := ctx.Value(userKey{}).(string)
	return userID, ok
}

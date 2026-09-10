package jobby

import (
	"crypto/rand"
	"strings"
)

// referenceAlphabet has exactly 64 characters, so masking a random byte with
// 63 selects each character with equal probability.
const referenceAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_"

// referenceLength is the number of random characters in a reference.
const referenceLength = 12

// Reference prefixes, one per kind of resource.
const (
	boardPrefix    = "bo"
	jobPrefix      = "jo"
	jobListPrefix  = "li"
	activityPrefix = "ac"
)

// newReference returns prefix, an underscore, and referenceLength random
// characters from referenceAlphabet, such as "bo_Xk3-a9Qz_Lp2".
func newReference(prefix string) string {
	buf := make([]byte, referenceLength)
	rand.Read(buf) // Never returns an error; see the crypto/rand docs.

	var b strings.Builder
	b.Grow(len(prefix) + 1 + referenceLength)
	b.WriteString(prefix)
	b.WriteByte('_')
	for _, c := range buf {
		b.WriteByte(referenceAlphabet[c&63])
	}
	return b.String()
}

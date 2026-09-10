package jobby

import "time"

// Clock supplies the current time. Constructors in this package take a Clock
// so that tests can fix the timestamps they produce.
type Clock interface {
	Now() time.Time
}

// SystemClock is a Clock that reads the system time.
type SystemClock struct{}

// Now returns the current time in UTC.
func (SystemClock) Now() time.Time { return time.Now().UTC() }

// FixedClock is a Clock that always returns T.
type FixedClock struct {
	T time.Time
}

// Now returns c.T.
func (c FixedClock) Now() time.Time { return c.T }

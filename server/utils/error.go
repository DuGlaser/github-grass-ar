package utils

type Error struct {
	Message string
}

func NewError(err error) *Error {
	return &Error{
		Message: err.Error(),
	}
}

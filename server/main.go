package main

import (
	"server/route"
)

func main() {
	router := route.Init()
	router.Start(":8080")
}

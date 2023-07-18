package main

import "fmt"

func main() {
	PrintHello()
}

//PrintHello :
//export PrintHello
func PrintHello() {
	fmt.Println("Hello From Golang")
}

package route

import (
	v1 "server/api/v1"

	"github.com/labstack/echo"
	"github.com/labstack/echo/middleware"
)

func Init() *echo.Echo {
	e := echo.New()

	e.Use(middleware.CORSWithConfig(middleware.CORSConfig{
		AllowOrigins: []string{"*"},
		AllowHeaders: []string{echo.HeaderOrigin, echo.HeaderContentType, echo.HeaderAcceptEncoding},
	}))

	apiV1 := e.Group("/api/v1")
	{
		apiV1.GET("/", v1.GetContributionsInfo)
	}

	return e
}

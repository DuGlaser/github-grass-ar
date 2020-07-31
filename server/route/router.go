package route

import (
	"server/api/v1/handlers"

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
		apiV1.GET("/get_contributions_info", handlers.GetContributionsInfo)
		apiV1.POST("/get_contributions_info", handlers.GetContributionsInfo)
	}

	return e
}

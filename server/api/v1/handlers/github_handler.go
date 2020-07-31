package handlers

import (
	"net/http"
	"server/api/v1/controllers"
	"server/utils"

	"github.com/labstack/echo"
)

type Params struct {
	Name string `query:"name"`
	From string `query:"from"`
	To   string `query:"to"`
}

func GetContributionsInfo(c echo.Context) error {
	p := new(Params)
	if err := c.Bind(p); err != nil {
		return c.JSON(http.StatusInternalServerError, utils.NewError(err))
	}

	data, err := controllers.ContributionsInfoQuery(p.Name, p.From, p.To)
	if err != nil {
		return c.JSON(http.StatusInternalServerError, utils.NewError(err))
	}

	return c.JSON(http.StatusOK, data)
}

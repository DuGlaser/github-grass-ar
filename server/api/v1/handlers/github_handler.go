package handlers

import (
	"net/http"
	"server/api/v1/controllers"

	"github.com/labstack/echo"
)

type Params struct {
	Name string `json: "name" from:"name" query:"name"`
	From string `json: "from" from:"from" query:"from"`
	To   string `json: "from" from:"from" query:"from"`
}

func GetContributionsInfo(c echo.Context) (err error) {
	p := new(Params)
	if err = c.Bind(p); err != nil {
		return
	}

	data, err := controllers.ContributionsInfoQuery(p.Name, p.From, p.To)
	if err != nil {
		return c.JSON(http.StatusInternalServerError, err)
	}

	return c.JSON(http.StatusOK, data)
}

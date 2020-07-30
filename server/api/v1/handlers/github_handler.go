package handlers

import (
	"net/http"

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

	// controllers.ContributionsInfoQuery(p.Name, p.From, p.To)

	return c.JSON(http.StatusOK, p)
}

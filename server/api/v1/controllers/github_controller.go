package controllers

import (
	"context"
	"server/auth"
	"server/model"
	"time"

	"github.com/shurcooL/githubv4"
)

func ContributionsInfoQuery(name, from, to string) (data model.Weeks, err error) {

	l := "2006-01-02"

	f, err := time.Parse(l, from)
	if err != nil {
		return data, err
	}

	githubTimeFrom := githubv4.NewDateTime(githubv4.DateTime{f})

	t, err := time.Parse(l, to)
	if err != nil {
		return data, err
	}

	githubTimeTo := githubv4.NewDateTime(githubv4.DateTime{t})

	var q struct {
		User struct {
			ContributionsCollection struct {
				ContributionCalendar struct {
					Weeks []struct {
						ContributionDays []struct {
							ContributionCount githubv4.Int
							Color             githubv4.String
						}
					}
				}
			} `graphql:"contributionsCollection(from: $from,to: $to)"`
		} `graphql:"user(login: $name)"`
	}

	varialble := map[string]interface{}{
		"from": githubv4.DateTime(*githubTimeFrom),
		"to":   githubv4.DateTime(*githubTimeTo),
		"name": githubv4.String(name),
	}

	client := auth.GithubAuth()

	err = client.Query(context.Background(), &q, varialble)
	if err != nil {
		return data, err
	}

	data = q.User.ContributionsCollection.ContributionCalendar.Weeks
	return data, nil
}

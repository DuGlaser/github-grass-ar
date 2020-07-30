package controllers

import (
	"context"
	"server/auth"
	"server/model"

	"github.com/shurcooL/githubv4"
)

func ContributionsInfoQuery(name string, from, to githubv4.DateTime) (err error, data model.ContributionsInfo) {
	var q struct {
		User struct {
			ContributionsCollection struct {
				ContributionCalendar struct {
					Weeks struct {
						ContributionDays struct {
							ContributionCount githubv4.Int
							Color             githubv4.String
						}
					}
				}
			} `graphql:"contributionsCollection(from: $from,to: $to)"`
		} `graphql:"user(login: $name)"`
	}

	varialble := map[string]interface{}{
		"from": githubv4.DateTime(from),
		"to":   githubv4.DateTime(to),
		"name": githubv4.String(name),
	}

	client := auth.GithubAuth()

	err = client.Query(context.Background(), &q, varialble)
	if err != nil {
		return err, data
	}

	data = q.User.ContributionsCollection.ContributionCalendar.Weeks.ContributionDays
	return nil, data
}

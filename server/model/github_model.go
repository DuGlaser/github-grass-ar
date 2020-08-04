package model

import "github.com/shurcooL/githubv4"

type Weeks []struct {
	ContributionDays []struct {
		ContributionCount githubv4.Int
		Color             githubv4.String
		Date              string
	}
}

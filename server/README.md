# server

## Setup

.envのGitHubのトークンの設定をする必要がある  

まず.evn.exampleをコピーして.envを作る
```zsh
cp .env.example .env
```

次に.envの中身を自分のGitHubトークンに書き換える
```.env
// xxxxxxxのところを自分のトークンにする
GITHUB_TOKEN = xxxxxxxxxxxxxxxxxxxxxxxxx
```

## Start
```zsh
go run main.go
```

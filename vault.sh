sleep 5 &&
curl -X POST 'http://bbt-tag-vault:8200/v1/secret/data/amorphie-tag' -H "Content-Type: application/json" -H "X-Vault-Token: admin" -d '{ "data": {"pass": "my-password", "username":"my-username","PostgreSql":"Host=localhost:5432;Database=TagDb;Username=postgres;Password=postgres"} }'

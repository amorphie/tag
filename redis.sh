redis-server --daemonize yes && sleep 3
redis-server -h 127.0.0.1 -p 6379
redis-cli MSET STATE_STORE 'amorphie-cache'
redis-cli MSET TEST_SERVER 'test'
redis-cli MSET config-amorphie-tag-db 'Host=localhost:5432;Database=tags;Username=postgres;Password=postgres'
redis-cli save 
redis-cli shutdown 
redis-server




redis-server --daemonize yes && sleep 3
redis-server -h 127.0.0.1 -p 6379
redis-cli MSET config-amorphie-transaction-db 'User ID=postgres;Password=postgres;Host=postgre;Port=5432;Database=transactions;'
redis-cli save 
redis-cli shutdown 
redis-server
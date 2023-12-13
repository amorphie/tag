redis-server --daemonize yes && sleep 3
redis-server -h localhost -p 6379
redis-cli MSET STATE_STORE 'amorphie-cache'
redis-cli MSET TEST_SERVER 'test'
redis-cli MSET config-amorphie-tag-db 'Host=localhost:5432;Database=TagDb;Username=postgres;Password=postgres'
redis-cli save 
redis-cli shutdown 
redis-server
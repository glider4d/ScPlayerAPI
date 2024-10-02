docker build -t scplayerapi .

docker run -d -p 8080:8080 --name scplayerapi_container scplayerapi

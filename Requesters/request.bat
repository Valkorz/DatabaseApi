:: Urls might change depending on your naming preferences
:: GET
curl http://localhost:5259/Task/get

:: POST
curl -X POST -H "Content-Type: application/json" -d "{\"name\":\"New Task\", \"isComplete\":false}" http://localhost:5259/Task/post
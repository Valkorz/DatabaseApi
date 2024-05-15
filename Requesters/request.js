//Replace with your dotnet generated local URL
const url = "http://localhost:5259";


function put(_name, _isComplete){
    let data = { name: _name, isComplete: _isComplete};
    let targetUrl = `${url}/task/post`;

    fetch(targetUrl, {
      method: 'POST', 
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    })
    .then(response => {
        if(!response.ok){
            throw new Error(`HTTP error! status: ${response.status} at ${targetUrl}`);
        }
        return response.json();
    })
    .then(data => {
      console.log('Success:', data);
    })
    .catch((error) => {
      console.error('Error:', error);
    }); 
}

function get(){
    fetch(`${url}/task/get`)
    .then(response => response.json())
    .then(data => console.log(data))
    .catch((error) => {
    console.error('Error:', error);
    });
}

put("test", true);
get();
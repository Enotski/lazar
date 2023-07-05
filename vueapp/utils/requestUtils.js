export const apiUrl = "https://localhost:7188/api";

export async function sendRequest(url, method = "GET", args = {}) {

  if(method === 'GET'){
    return await fetch(url, {method: 'GET'})
      .then((response) => response.json())
      .catch((ex) => {
        console.log(ex);
        throw new Error("Data Loading Error");
      });  
  }

  return await fetch(url, {
    method: method,
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(args),
  })
    .then((response) => response.json())
    .catch((ex) => {
      console.log(ex);
      throw new Error("Data Loading Error");
    });
}

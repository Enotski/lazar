export const apiUrl = "https://localhost:7188/api";
import authHeader from "@/services/authHeader";

export async function sendRequest(url, method = "GET", args = {}, authToken) {
  let headers = {
    "Content-Type": "application/json",
  }
  authToken = true;
  let token = authHeader();
  if(authToken !== undefined){
    headers = {
      "Content-Type": "application/json",
      "Authorization": token
    }
  }

  if (method === "GET") {
    return await fetch(url, {
      method: "GET",
      headers: headers
    })
      .then((response) => response.json())
      .catch((ex) => {
        console.log(ex);
        throw new Error("Data Loading Error");
      });
  }

  return await fetch(url, {
    method: method,
    headers: headers,
    body: JSON.stringify(args),
  })
    .then((response) => response.json())
    .catch((ex) => {
      console.log(ex);
      throw new Error("Data Loading Error");
    });
}

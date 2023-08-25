export const apiUrl = "https://localhost:7188/api";
import authHeader from "@/services/authHeader";
import axios from "axios";

export async function sendRequest(url, method = "GET", args = {}, authToken) {
  let headers = {
    "Content-Type": "application/json",
  }
  authToken = true;
  let token = authHeader();
  if(authToken !== undefined){
    headers["Authorization"] = token;
  }

  if (method === "GET") {
    return await axios.get(url, headers)
    .then((response) => response)
    .catch((ex) => {
      console.log(ex);
      throw new Error("Data Loading Error");
    });
    // return await fetch(url, {
    //   method: "GET",
    //   headers: headers
    // })
    //   .then((response) => response.json())
    //   .catch((ex) => {
    //     console.log(ex);
    //     throw new Error("Data Loading Error");
    //   });
  }
  return await axios.post(url, args, headers)
  .then((response) => {  
    return response.data;
  })
  .catch((ex) => {
    console.log(ex);
    throw new Error("Data Loading Error");
  });

  // return await fetch(url, {
  //   method: method,
  //   headers: headers,
  //   body: JSON.stringify(args),
  // })
    
}

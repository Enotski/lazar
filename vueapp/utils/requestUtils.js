export const apiUrl = "https://localhost:7188/api";

export async function sendRequest(url, method = "GET", args = {}) {
  return await fetch(url, {
    method: method,
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(args),
  })
    .then((response) => response.json())
    .catch(() => {
      throw new Error("Data Loading Error");
    });
}

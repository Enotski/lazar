export async function sendRequest(url, method = "GET", args = {}) {
  const params = Object.keys(data)
    .map((key) => `${encodeURIComponent(key)}=${encodeURIComponent(args[key])}`)
    .join("&");

  if (method === "GET") {
    return fetch(url, {
      method,
      credentials: "include",
    }).then((result) =>
      result.json().then((json) => {
        if (result.ok) return json.data;
        throw json.Message;
      })
    );
  }

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

export const apiUrl = "http://localhost:8080/api";
import authHeader from "@/services/authHeader";
import axios from "axios";
import authService from "@/services/authService";

let refreshingFunc = undefined;

const instance = axios.create({
  baseURL: apiUrl,
  headers: {
    "Content-Type": "application/json",
  },
});

instance.interceptors.request.use(
  (config) => {
    let token = authHeader();
    if (token) {
      config.headers["Authorization"] = token;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);
instance.interceptors.response.use(
  (res) => {
    return res;
  },
  async (err) => {
    const originalConfig = err.config;

    if (err.response) {
      // Access Token was expired
      if (err.response.status === 401 && !originalConfig._retry) {
        originalConfig._retry = true;
        try {
          // the trick here, that `refreshingFunc` is global, e.g. 2 expired requests will get the same function pointer and await same function.
          if (!refreshingFunc) refreshingFunc = authService.refreshToken();

          const rs = await refreshingFunc;
          let user = JSON.parse(window.localStorage.getItem("user"));
          user = rs;
          localStorage.setItem("user", JSON.stringify(user));
          instance.defaults.headers.common["Authorization"] = authHeader();
          return instance(originalConfig);
        } catch (_error) {
          if (_error.response && _error.response.data) {
            return Promise.reject(_error.response.data);
          }

          return Promise.reject(_error);
        } finally {
          refreshingFunc = undefined;
        }
      }

      if (err.response.status === 403 && err.response.data) {
        return Promise.reject(err.response.data);
      }
    }

    return Promise.reject(err);
  }
);

export async function sendRequest(url, method = "GET", args = {}) {
  if (method === "GET") {
    return await instance
      .get(url)
      .then((response) => {
        if (response.data.Result !== undefined && response.data.Result !== 0)
          throw response.data.Message;
        return response.data;
      })
      .catch((ex) => {
        console.log(ex);
        throw ex;
      });
  }
  return await instance
    .post(url, args)
    .then((response) => {
      if (response.data.Result !== undefined && response.data.Result !== 0)
        throw response.data.Message;
      return response.data;
    })
    .catch((ex) => {
      console.log(ex);
      throw ex;
    });
}

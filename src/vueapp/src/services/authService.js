import { sendRequest } from "@/utils/requestUtils";

class AuthService {
  async login(params) {
    localStorage.removeItem("user");
    return await sendRequest(`/auth/log-in`, "POST", params)
      .then((response) => {
        if (response.Tokens.AccessToken) {
          localStorage.setItem("user", JSON.stringify(response));
        }

        return response;
      })
      .catch((ex) => {
        throw ex;
      });
  }
  async logout() {
    let user = JSON.parse(localStorage.getItem("user"));
    this.removeUserTokens();
    await sendRequest(`/auth/log-out`, "POST", { login: user.Login })
      .then((resp) => {
        localStorage.removeItem("user");
        return resp;
      })
      .catch((ex) => {
        throw ex;
      });
  }
  async register(params) {
    localStorage.removeItem("user");
    return await sendRequest(`/auth/register`, "POST", params)
      .then((response) => {
        if (response.Tokens.AccessToken) {
          localStorage.setItem("user", JSON.stringify(response));
        }
        return response;
      })
      .catch((ex) => {
        throw ex;
      });
  }
  async refreshToken() {
    let tokens = this.removeUserTokens();
    return await sendRequest("/auth/refresh", "POST", tokens).catch((ex) => {
      throw ex;
    });
  }
  removeUserTokens() {
    let user = JSON.parse(localStorage.getItem("user"));
    let tokens = user.Tokens;
    user.Tokens = null;
    localStorage.setItem("user", JSON.stringify(user));
    return tokens;
  }
}

export default new AuthService();

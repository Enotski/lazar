import {sendRequest, apiUrl} from "../../utils/requestUtils";

class AuthService {
    async login(params) {
        return await sendRequest(`${apiUrl}/auth/log-in`, "POST", params)
        .then(response => {
          if (response.access_token) {
            localStorage.setItem('user', JSON.stringify(response));
          }
  
          return response;
        })
        .catch(() => {
          throw new Error("Data Loading Error");
        });
    } 
    logout() {
      localStorage.removeItem('user');
    }
    async register(params) {
      return await sendRequest(`${apiUrl}/auth/sign-up`, "POST", params)
        .then(response => {
          if (response.access_token) {
            localStorage.setItem('user', JSON.stringify(response));
          }
  
          return response;
        })
        .catch(() => {
          throw new Error("Data Loading Error");
        });
    }
  }
  
  export default new AuthService();
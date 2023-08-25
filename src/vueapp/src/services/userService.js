import authHeader from './authHeader';
import {sendRequest, apiUrl} from "../../utils/requestUtils";

class UserService {
  getPublicContent() {
    return sendRequest(`${apiUrl}/`);
  }

  getUserBoard() {
    return axios.get(apiUrl + 'user', { headers: authHeader() });
  }

  getModeratorBoard() {
    return axios.get(apiUrl + 'mod', { headers: authHeader() });
  }

  getAdminBoard() {
    return axios.get(apiUrl + 'admin', { headers: authHeader() });
  }
}

export default new UserService();
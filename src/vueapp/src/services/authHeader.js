export default function authHeader() {
    let user = JSON.parse(localStorage.getItem('user'));
  
    if (user && user.Tokens) {
      return 'Bearer ' + user.Tokens.AccessToken ;
    } else {
      return {};
    }
  }
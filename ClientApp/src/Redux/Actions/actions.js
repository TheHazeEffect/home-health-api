
export const LOGIN = 'LOGIN';
export const LOGOUT = 'LOGOUT';

export function LoginUser(email, firstName,roleName,token) {
  return { 
      type: LOGIN,
      email: email,
      firstName: firstName,
      roleName: roleName,
      token : token,
    };
}

export function LogoutUser() {
    return {type:LOGOUT}
}
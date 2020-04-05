
export const LOGIN = 'LOGIN';
export const LOGOUT = 'LOGOUT';

export function LoginUser(id,email, firstName,roleName,token) {
  return { 
      type: LOGIN,
      id : id,
      email: email,
      firstName: firstName,
      roleName: roleName,
      token : token,
    };
}

export function LogoutUser() {
    return {type:LOGOUT}
}
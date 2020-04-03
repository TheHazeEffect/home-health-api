
export const LOGIN = 'LOGIN';
export const LOGOUT = 'LOGOUT';

export function LoginUser(email, roleName,token) {
  return { type: LOGIN, email: email, token : token, roleName: roleName };
}

export function LogoutUser() {
    return {type:LOGOUT}
}
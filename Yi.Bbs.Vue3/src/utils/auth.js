import useUserStore from "@/stores/user";
const TokenKey = "Admin-Token";

export function getToken() {
  return localStorage.getItem(TokenKey);
}

export function setToken(token) {
  return localStorage.setItem(TokenKey, token);
}

export function removeToken() {
  return localStorage.removeItem(TokenKey);
}

export function getPermission(code) {
  const all_permission = "*:*:*";
  const isHasPermission = useUserStore().permissions.some((permission) => {
    return all_permission === permission || code.includes(permission);
  });
  return {
    isHasPermission,
  };
}

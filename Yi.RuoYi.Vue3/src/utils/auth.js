import Cookies from 'js-cookie'

const TokenKey = 'Admin-Token'
const TenantIdKey='Tenant-Id'
export function getToken() {
  return localStorage.get(TokenKey)
}

export function setToken(token) {
  return localStorage.set(TokenKey, token)
}

export function removeToken() {
  return localStorage.remove(TokenKey)
}
export function getTenantId() {
  return localStorage.get(TenantIdKey)
}

export function setTenantId(tenantId) {
  return localStorage.set(TenantIdKey, tenantId)
}

export function removeTenantId() {
  return localStorage.remove(TenantIdKey)
}

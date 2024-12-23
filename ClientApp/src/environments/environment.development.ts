import { domain, clientId } from '../../auth_config.json'

export const environment = {
  production: false,
  API_URL_BASE: "http://localhost:5240",
  auth: {
    domain,
    clientId,
    authorizationParams: {
      redirect_uri: window.location.origin,
      audience: 'http://localhost:3000'
    }
  }
};

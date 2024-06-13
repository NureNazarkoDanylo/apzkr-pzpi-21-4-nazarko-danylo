import { axiosAuth } from '@/app/_lib/api/axios';
import useAuth from './useAuth';
import { TokensModel } from '../types/dtos';

export default function useRefreshToken() {

  const { tokens, setAuth } = useAuth();

  const refresh = async () => {

    try {
      const response = await axiosAuth.post<TokensModel>('/identity/renewAccessTokenWithCookie', {}, { withCredentials: true });
      
      // console.log('Old: \n' + JSON.stringify(tokens));
      // console.log('New: \n' + JSON.stringify(response.data));

      setAuth(response.data);
      return response.data;
    } catch (error) {
      console.error('Failed to renew access token. Check your refresh token cookie.')
    }
  }

  return refresh;
}

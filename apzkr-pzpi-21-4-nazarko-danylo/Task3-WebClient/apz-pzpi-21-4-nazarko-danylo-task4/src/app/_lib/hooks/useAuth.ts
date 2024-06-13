import { useContext } from 'react';
import { AuthContext, AuthContextModel } from '@/app/_lib/context/AuthProvider';

const useAuth = () => {
  return useContext(AuthContext) as AuthContextModel;
}

export default useAuth;

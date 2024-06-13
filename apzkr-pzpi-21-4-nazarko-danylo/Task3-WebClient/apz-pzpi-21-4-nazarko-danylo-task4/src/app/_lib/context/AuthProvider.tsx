import { createContext, useState } from "react";
import { JwtPayload, jwtDecode } from 'jwt-decode'
import { TokensModel } from "../types/dtos";

export type AuthContextModel = {
  id: string | undefined;
  email: string | undefined;
  roles: string[] | undefined;
  expiresOn: number | undefined;
  tokens: TokensModel | null;
  setAuth: (tokesn: TokensModel) => void;
};

export const AuthContext = createContext<AuthContextModel | null>(null);

export function AuthProvider({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {

  const [tokens, setTokens] = useState<TokensModel | null>(null);
  const [id, setId] = useState<string | undefined>(undefined);
  const [email, setEmail] = useState<string | undefined>(undefined);
  const [roles, setRoles] = useState<string[] | undefined>(undefined);
  const [expiresOn, setExpiresOn] = useState<number | undefined>(undefined);

  const setAuth = (tokens: TokensModel) => {
    setTokens(tokens);

    const decodedJwt: any = jwtDecode<JwtPayload>(tokens?.accessToken as string);
    setId(decodedJwt.sub);
    setEmail(decodedJwt.email);
    setRoles(decodedJwt.roles);
    setExpiresOn(decodedJwt.exp);

    // console.log(decodedJwt);
  };

  return (
    <AuthContext.Provider value={{ tokens: tokens, setAuth: setAuth, id: id, email: email, roles: roles, expiresOn: expiresOn }}>
      {children}
    </AuthContext.Provider>
  );
}

'use client'

import useRefreshToken from '@/app/_lib/hooks/useRefreshToken';

export default function Home() {

  const refreshAccessToken = useRefreshToken()

  return (
    <>
      <h1>Main Page</h1>
      <br /><button onClick={refreshAccessToken}>Refresh Access Token</button>
    </>
  );
}

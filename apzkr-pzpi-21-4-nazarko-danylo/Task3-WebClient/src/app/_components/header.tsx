import styles from '../_styles/header.module.css'
import Link from 'next/link'
import Image from 'next/image'
import { usePathname } from 'next/navigation'
import useAuth from '@/app/_lib/hooks/useAuth'
import useRefreshToken from '@/app/_lib/hooks/useRefreshToken';
import { useEffect } from 'react'
import useAxiosAuth from '../_lib/hooks/useAxiosAuth'

function LeftSectionLinks({ isUserLoggedIn }: { isUserLoggedIn: boolean }) {
  return (
    <>
      <Link className={styles.itemContainer} href={'/'}>Home</Link>
      {isUserLoggedIn && 
        <>
          <Link className={styles.itemContainer} href={'/washingMachines'}>Manage</Link>
          <Link className={styles.itemContainer} href={'/washingMachines/discover'}>Discover</Link>
        </>
      }
    </>
  )
}

function RightSectionLinks({ isUserLoggedIn }: { isUserLoggedIn: boolean }) {

  const axiosAuth = useAxiosAuth();

  async function logout() {
    await axiosAuth.post('/identity/revokeRefreshTokenWithCookie', {}, { withCredentials: true });
  }

  const pathName = usePathname();

  if (!isUserLoggedIn) {
    return (
      <Link
        className={`${styles.itemContainer} ${pathName === '/login' ? styles.active : ''}`}
        href={'/login'}>
        Sign In
      </Link>);
  }

  // return (
  //   <a className={styles.itemContainer} onClick={logout}>Logout</a>
  // )
}

export default function Header() {

  const { tokens } = useAuth();

  const isUserLoggedIn: boolean = tokens?.accessToken !== undefined;

  const refreshAccessToken = useRefreshToken()

  useEffect(() => {
    refreshAccessToken();
  }, [])

  return (
    <header className={styles.header}>
      <div className={styles.leftSection}>
        <Link className={styles.itemContainer} href={'/'}>
          {'logo'/* <Image src={'/placeholder.svg'} alt={'Logo icon placeholder'} width={32} height={32} style={{ display: 'inline' }} /> */}
        </Link>
        <LeftSectionLinks isUserLoggedIn={isUserLoggedIn} />
      </div>
      <div className={styles.rightSection}>
        <RightSectionLinks isUserLoggedIn={isUserLoggedIn} />
      </div>
    </header>
  );
}

'use client'

import styles from './_styles/page.module.css'
import LoginForm from './_components/loginForm'
import { redirect } from 'next/navigation'
import useAuth from '@/app/_lib/hooks/useAuth';

export default function Login() {

  const { tokens } = useAuth();

  if (tokens?.accessToken !== undefined) {
    redirect('/');
  }

  return (
    <div className={styles.mainContainer}>
      <LoginForm />
    </div>
  )
}

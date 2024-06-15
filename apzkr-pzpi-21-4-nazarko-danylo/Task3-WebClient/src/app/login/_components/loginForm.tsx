'use client'

import styles from '../_styles/loginForm.module.css'
import Link from 'next/link'
import { useState } from 'react';
import axios from '@/app/_lib/api/axios'
import { TokensModel } from '@/app/_lib/types/dtos'

import useAuth from '@/app/_lib/hooks/useAuth';

export default function LoginForm() {

  const { setAuth } = useAuth();

  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');

  const [errorMessage, setErrorMessage] = useState<string | undefined>(undefined);

  const handleSubmit = async (event: { preventDefault: () => void; }) => {
    event.preventDefault();

    try {
      const response = await axios.post('/identity/loginWithCookie',
        JSON.stringify({ email: email, password: password }),
        {
          headers: { 'Content-Type': 'application/json' },
          withCredentials: true
        });

      if (response.status !== 200) {
        setErrorMessage(response.data.detail);
        return;
      }

      // console.log('LoginForm: ' + JSON.stringify(response.data));
      setAuth({ accessToken: response.data.accessToken, refreshToken: response.data.refreshToken });
    } catch (error: any) {
      if (!error.response) {
        setErrorMessage('No Server Response');
        return;
      }

      // const errors = error.response.data.errors;
      // const hasErrors = errors !== undefined;
      //
      // console.log(errors);
      // 
      // if (hasErrors) {
      //   let displayedErrorMessage: string = '';
      //
      //   for (const errorFieldName in errors) {
      //     displayedErrorMessage += errorFieldName + ':\n';
      //     for (const errorMessage of errors[errorFieldName]) {
      //       displayedErrorMessage += ' - ' + errorMessage + '\n';
      //     }
      //   }
      //   
      //   setErrorMessage(displayedErrorMessage);
      //   return;
      // }

      setErrorMessage(error.response.data.detail);
    }
  }

  return (
    <>
      <div className={styles.formContainer}>
        <div className={styles.formHeader}>
          <span>Sign In</span>
        </div>
        <form className={styles.form} onSubmit={handleSubmit}>
          <div className={styles.inputContainer}>
            <input className={styles.input} value={email} onChange={(e) => setEmail(e.target.value)} type="text" name="email" id="email" placeholder="Enter your email address" />
          </div>
          <div className={styles.inputContainer}>
            <input className={styles.input} value={password} onChange={(e) => setPassword(e.target.value)} type="password" name="password" id="password" placeholder="Enter your password" />
          </div>
          <div className={styles.buttonContainer}>
            <button className={styles.button} type="submit">Sign In</button>
            <Link className={styles.resetPasswordLink} href={'resetPassword'}>Forgot password?</Link>
          </div>
        </form>
      </div>

      {
        errorMessage ?
          <>
            <br />
            <div className={styles.formContainer}>
              <div className={styles.formHeader}>
                <span>{errorMessage}</span>
              </div>
            </div>
          </> :
          <></>
      }
    </>
  )
}

'use client'

import type { Metadata } from 'next';
import './globals.css';
import styles from './_styles/layout.module.css'
import Header from './_components/header'
import Footer from './_components/footer'

import { AuthProvider } from '@/app/_lib/context/AuthProvider'
import { CookiesProvider } from 'react-cookie';

// export const metadata: Metadata = {
//   title: "Expense Tracker",
// };

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <CookiesProvider defaultSetOptions={{ path: '/' }}>
          <AuthProvider>
            <Header />
            <main className={styles.main}>
              {children}
            </main>
            <Footer />
          </AuthProvider>
        </CookiesProvider>
      </body>
    </html>
  );
}

'use client'

import styles from '../_styles/footer.module.css'
import Link from 'next/link'

export default function Footer() {
  return (
    <footer className={styles.footer}>
      <div className={styles.centerSection}>
        <Link className={styles.itemContainer} href={'https://cuqmbr.xyz'}>
          Footer Link
        </Link>
      </div>
    </footer>
  );
}

import styles from '@/app/login/_styles/loginForm.module.css'
import WashingMachineDetails from './_components/WashingMachineDetails'

export default function Details() {
  return (
    <div className={styles.mainContainer}>
      <WashingMachineDetails />
    </div>
  )
}

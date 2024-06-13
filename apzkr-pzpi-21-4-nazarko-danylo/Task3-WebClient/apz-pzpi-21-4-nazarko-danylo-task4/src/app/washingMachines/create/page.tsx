import styles from '@/app/login/_styles/loginForm.module.css'
import CreateWashingMachineForm from "./_components/createWashingMachineForm";

export default function Login() {
  return (
    <div className={styles.mainContainer}>
      <CreateWashingMachineForm />
    </div>
  )
}

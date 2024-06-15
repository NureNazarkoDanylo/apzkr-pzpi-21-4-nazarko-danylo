import styles from '@/app/login/_styles/loginForm.module.css'
import EditWashingMachineForm from "./_components/editWashingMachineForm";

export default function Edit() {
  return (
    <div className={styles.mainContainer}>
      <EditWashingMachineForm />
    </div>
  )
}

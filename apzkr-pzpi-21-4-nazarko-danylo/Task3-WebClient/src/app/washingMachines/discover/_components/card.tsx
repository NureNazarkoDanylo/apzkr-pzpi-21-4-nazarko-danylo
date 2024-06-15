import { WashingMachine } from "@/app/_lib/types/dtos";
import Link from "next/link";
import styles from '../../_styles/card.module.css'


export default function Card({
  washingMachine
}: Readonly<{
  washingMachine: WashingMachine;
}>) {
  return (
    <div className={styles.card}>
      <div className={styles.leftSection}>
        <div className={styles.name}>
          Name: {washingMachine.name}
        </div>
        <div className={styles.manufacturer}>
          Manufacturer: {washingMachine.manufacturer}
        </div>
        <div className={styles.serialNumber}>
          Serial number: {washingMachine.serialNumber}
        </div>
      </div>
      <br />
      <div className={styles.rightSection}>
        <Link className={styles.linkButton}
          href={`/washingMachines/create` +
            `?id=${washingMachine.id}` +
            `&name=${washingMachine.name}` +
            `&manufacturer=${washingMachine.manufacturer}` +
            `&serialNumber=${washingMachine.serialNumber}` +
            `&description=${washingMachine.description ?? ''}` +
            `&deviceGroupId=${washingMachine.deviceGroupId ?? ''}`}>
          create
        </Link>
        <br />
      </div>
    </div>
  )
}

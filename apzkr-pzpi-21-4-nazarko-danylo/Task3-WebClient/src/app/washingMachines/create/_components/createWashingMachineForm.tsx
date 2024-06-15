'use client'

import styles from '@/app/login/_styles/loginForm.module.css'
import Link from 'next/link'
import { useEffect, useState } from 'react';
import useAuth from '@/app/_lib/hooks/useAuth';
import { WashingMachine } from '@/app/_lib/types/dtos';
import useAxiosAuth from '@/app/_lib/hooks/useAxiosAuth';

export default function CreateWashingMachineForm() {

  const { setAuth } = useAuth();
  const axiosAuth = useAxiosAuth();

  const [id, setId] = useState<string | null>();
  const [name, setName] = useState<string | null>();
  const [manufacturer, setManufacturer] = useState<string | null>();
  const [serialNumber, setSerialNumber] = useState<string | null>();
  const [description, setDescription] = useState<string | null>();
  const [deviceGroupId, setDeviceGroupId] = useState<string | null>();

  useEffect(() => {
    const urlSearchString = window.location.search;
    const params = new URLSearchParams(urlSearchString);

    setId(params.get('id'));
    setName(params.get('name'));
    setManufacturer(params.get('manufacturer'));
    setSerialNumber(params.get('serialNumber'));
    setId(params.get('id'));
  }, []);

  const [success, setSuccess] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string | undefined>(undefined);

  const handleSubmit = async (event: { preventDefault: () => void; }) => {
    event.preventDefault();
    setErrorMessage(undefined);

    try {
      const response = await axiosAuth.post('/washingMachines',
        JSON.stringify({ id, name, manufacturer, serialNumber, description, deviceGroupId } as WashingMachine),
        {
          headers: { 'Content-Type': 'application/json' },
          withCredentials: true
        });

      // console.log('CreateWashingMachineForm: ' + JSON.stringify(response.data));
      setSuccess(true);
    } catch (error: any) {
      if (!error.response) {
        setErrorMessage('No Server Response');
        return;
      }

      const errors = error.response.data.errors;
      const hasErrors = errors !== undefined;

      // console.log(errors);

      if (hasErrors) {
        let displayedErrorMessage: string = '';

        for (const errorFieldName in errors) {
          // displayedErrorMessage += errorFieldName + ':\n';
          for (const errorMessage of errors[errorFieldName]) {
            displayedErrorMessage += /* ' - ' +  */errorMessage + '\n';
          }
        }

        setErrorMessage(displayedErrorMessage);
        return;
      }

      setErrorMessage(error.response.data.title + ' ' + error.response.data.detail);
    }
  }

  return (
    <>
      <div className={styles.formContainer}>
        <div className={styles.formHeader}>
          <span>Create Washing Machine</span>
        </div>
        <form className={styles.form} onSubmit={handleSubmit}>
          <div className={styles.inputContainer}>
            <input className={styles.input} value={id ?? ''} onChange={(e) => setId(e.target.value)} type="text" name="id" id="id" placeholder="Enter device id" />
          </div>
          <div className={styles.inputContainer}>
            <input className={styles.input} value={name ?? ''} onChange={(e) => setName(e.target.value)} type="name" name="name" id="name" placeholder="Enter device name" />
          </div>
          <div className={styles.inputContainer}>
            <input className={styles.input} value={manufacturer ?? ''} onChange={(e) => setManufacturer(e.target.value)} type="text" name="manufacturer" id="manufacturer" placeholder="Enter device manufacturer" />
          </div>
          <div className={styles.inputContainer}>
            <input className={styles.input} value={serialNumber ?? ''} onChange={(e) => setSerialNumber(e.target.value)} type="text" name="serialNumber" id="serialNumber" placeholder="Enter device serialNumber" />
          </div>
          <div className={styles.inputContainer}>
            <input className={styles.input} value={description ?? ''} onChange={(e) => setDescription(e.target.value)} type="text" name="description" id="description" placeholder="Enter device description" />
          </div>
          <div className={styles.inputContainer}>
            <input className={styles.input} value={deviceGroupId ?? ''} onChange={(e) => setDeviceGroupId(e.target.value)} type="text" name="deviceGroupId" id="deviceGroupId" placeholder="Enter device deviceGroupId" />
          </div>
          <div className={styles.buttonContainer}>
            <button className={styles.button} type="submit">Create</button>
            <Link className={styles.resetPasswordLink} href={'/washingMachines'}>Back</Link>
          </div>
        </form>
      </div>

      {
        errorMessage ?
          <>
            <br />
            <div className={styles.formContainer}>
              <div className={styles.formHeader}>
                {errorMessage.split('\n').map(s => <><span>{s}</span><br /></>)}
              </div>
            </div>
          </> :
          <></>
      }

      {
        success ?
          <>
            <br />
            <div className={styles.formContainer}>
              <div className={styles.formHeader}>
                <span>Success!</span>
                <br />
              </div>
            </div>
          </> :
          <></>
      }
    </>
  )
}

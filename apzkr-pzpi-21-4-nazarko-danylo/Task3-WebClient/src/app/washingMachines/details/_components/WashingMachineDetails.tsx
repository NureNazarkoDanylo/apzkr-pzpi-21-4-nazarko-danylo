'use client'

import styles from '@/app/login/_styles/loginForm.module.css'
import Link from 'next/link'
import { useEffect, useState } from 'react';
import useAxiosAuth from '@/app/_lib/hooks/useAxiosAuth';
import { WashingMachine, WashingMachineOperationStatus, WashingMachineState } from '@/app/_lib/types/dtos';

export default function WashingMachineDetails() {

  const axiosAuth = useAxiosAuth();

  const [id, setId] = useState<string | null>();
  const [name, setName] = useState<string | null>();
  const [manufacturer, setManufacturer] = useState<string | null>();
  const [serialNumber, setSerialNumber] = useState<string | null>();
  const [description, setDescription] = useState<string | null>();
  const [deviceGroupId, setDeviceGroupId] = useState<string | null>();

  const [statusData, setStatusData] = useState<WashingMachineOperationStatus>();
  const [isStatusConnected, setIsStatusConnected] = useState<boolean>(false);
  const [statusErrorMesasage, setStatusErrorMessage] = useState<string | undefined>(undefined);

  useEffect(() => {
    const urlSearchString = window.location.search;
    const params = new URLSearchParams(urlSearchString);

    const id = params.get('id');
    setId(id);
    setName(params.get('name'));
    setManufacturer(params.get('manufacturer'));
    setSerialNumber(params.get('serialNumber'));
    const paramDescription = params.get('description');
    setDescription(paramDescription === '' ? null : paramDescription);
    const paramDeviceGroupId = params.get('deviceGroupId');
    setDeviceGroupId(paramDeviceGroupId === '' ? null : paramDeviceGroupId);
    setId(params.get('id'));

    const eventSource = new EventSource(`http://localhost:5000/washingMachines/${id}/streamStatus`);

    eventSource.onopen = (event) => {
      setIsStatusConnected(true);
    }

    eventSource.addEventListener("status", (event) => {
      const newData = JSON.parse(event.data) as WashingMachineOperationStatus;
      console.log(newData);
      setStatusData(newData);
    });

    eventSource.onerror = (err) => {
      // setIsError(true);
      console.error(err);
    };

    return () => {
      eventSource.close();
    };
  }, []);

  const [deleteSuccess, setDeletetSuccess] = useState<boolean>(false);
  const [deleteErrorMessage, setDeleteErrorMessage] = useState<string | undefined>(undefined);

  const handleSubmit = async (event: { preventDefault: () => void; }) => {
    event.preventDefault();
    setDeleteErrorMessage(undefined);

    // setStatusData({state: WashingMachineState.Prewash, waterTemperatureCelcius: 10, motorSpeedRPM: 50, isLidClosed: true, loadWeightKg: 5} as WashingMachineOperationStatus);
    // return;
    try {
      const response = await axiosAuth.delete(`/washingMachines/${id}`,
        {
          headers: { 'Content-Type': 'application/json' },
          withCredentials: true
        });

      // console.log('CreateWashingMachineForm: ' + JSON.stringify(response.data));
      setDeletetSuccess(true);
      setIsStatusConnected(false);
    } catch (error: any) {
      if (!error.response) {
        setDeleteErrorMessage('No Server Response');
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

        setDeleteErrorMessage(displayedErrorMessage);
        return;
      }

      setDeleteErrorMessage(error.response.data.title + ' ' + error.response.data.detail);
    }
  }

  return (
    <>
      <div className={styles.formContainer}>
        <div className={styles.formHeader}>
          <span>Washing Machine Details</span>
        </div>
        <form className={styles.form} onSubmit={handleSubmit}>
          <div className={styles.inputContainer}>
            <input disabled className={styles.input} value={id ?? ''} onChange={(e) => setId(e.target.value)} type="text" name="id" id="id" placeholder="Enter device id" />
          </div>
          <div className={styles.inputContainer}>
            <input disabled className={styles.input} value={name ?? ''} onChange={(e) => setName(e.target.value)} type="name" name="name" id="name" placeholder="Enter device name" />
          </div>
          <div className={styles.inputContainer}>
            <input disabled className={styles.input} value={manufacturer ?? ''} onChange={(e) => setManufacturer(e.target.value)} type="text" name="manufacturer" id="manufacturer" placeholder="Enter device manufacturer" />
          </div>
          <div className={styles.inputContainer}>
            <input disabled className={styles.input} value={serialNumber ?? ''} onChange={(e) => setSerialNumber(e.target.value)} type="text" name="serialNumber" id="serialNumber" placeholder="Enter device serialNumber" />
          </div>
          <div className={styles.inputContainer}>
            <input disabled className={styles.input} value={description ?? ''} onChange={(e) => setDescription(e.target.value)} type="text" name="description" id="description" placeholder="Enter device description" />
          </div>
          <div className={styles.inputContainer}>
            <input disabled className={styles.input} value={deviceGroupId ?? ''} onChange={(e) => setDeviceGroupId(e.target.value)} type="text" name="deviceGroupId" id="deviceGroupId" placeholder="Enter device deviceGroupId" />
          </div>
          <div className={styles.buttonContainer}>
            <button className={styles.button} type="submit">Delete</button>
            <Link className={styles.resetPasswordLink} href={'/washingMachines'}>Back</Link>
          </div>
        </form>
      </div>

      {
        isStatusConnected ?
          <>
            <br />
            <div className={styles.formContainer}>
              <div className={styles.formHeader}>
                Status
              </div>
              <p>State: {WashingMachineState[statusData!.state]}</p>
              <p>Water Temperature: {statusData?.waterTemperatureCelcius}</p>
              <p>Load Weight: {statusData?.loadWeightKg}</p>
              <p>Motor Speed: {statusData?.motorSpeedRPM}</p>
              <p>Lid Closed: {statusData?.isLidClosed.toString()}</p>
            </div>
          </> :
          <>
            <br />
            <div className={styles.formContainer}>
              <div className={styles.formHeader}>
                No Connection To Washing Machine
              </div>
            </div>
          </>
      }

      {
        deleteErrorMessage ?
          <>
            <br />
            <div className={styles.formContainer}>
              <div className={styles.formHeader}>
                {deleteErrorMessage.split('\n').map(s => <><span>{s}</span><br /></>)}
              </div>
            </div>
          </> :
          <></>
      }

      {
        deleteSuccess ?
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


'use client'

import { useEffect, useState } from "react";
import useAxiosAuth from "../../_lib/hooks/useAxiosAuth";
import { PaginatedList, WashingMachine } from "../../_lib/types/dtos";
import Link from 'next/link'
import Card from "./_components/card";
import styles from '.././_styles/page.module.css'
import Divider from ".././_components/divider";


export default function DiscoverWashingMachines() {

  const axiosAuth = useAxiosAuth();

  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [isError, setIsError] = useState<boolean>(false);
  const [data, setData] = useState<WashingMachine[]>(new Array<WashingMachine>());

  useEffect(() => {
    const eventSource = new EventSource("http://localhost:5000/washingMachines/discover");

    eventSource.addEventListener("discovered", (event) => {
      const newItem = JSON.parse(event.data) as WashingMachine;
      let newData = data?.copyWithin(0, 0);
      // console.log(newData);
      newData?.unshift(newItem);
      setData(newData);
    });

    eventSource.onerror = (err) => {
      setIsError(true);
      console.error(err);
    };

    setTimeout(() => { eventSource.close(); setIsLoading(false); }, 1000);

    return () => {
      eventSource.close();
    };
  }, []);

  return (
    <div className={styles.wrapper}>
      {isLoading ?
        <h1>Loading...</h1> :
        isError ?
          <h1>Error!</h1> :
          <>
            <div className={styles.cards}>
              {data?.map(
                (washingMachine) => {
                  return (
                    <>
                      <Card washingMachine={washingMachine} key={washingMachine.id} />
                      <Divider />
                    </>
                  );
                })}
            </div>
          </>
      }
    </div>
  );
}

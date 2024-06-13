'use client'

import { useEffect, useState } from "react";
import useAxiosAuth from "../_lib/hooks/useAxiosAuth";
import { PaginatedList, WashingMachine } from "../_lib/types/dtos";
import Link from 'next/link'
import Card from "./_components/card";
import styles from './_styles/page.module.css'
import Divider from "./_components/divider";


export default function WashingMachines() {

  const axiosAuth = useAxiosAuth();

  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [isError, setIsError] = useState<boolean>(false);
  const [data, setData] = useState<PaginatedList<WashingMachine>>();

  const [pageNumber, setPageNumber] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(3);

  useEffect(() => {
    // setIsLoading(true);
    // setIsError(false);

    // const urlSearchString = window.location.search;
    // const params = new URLSearchParams(urlSearchString);
    //
    // const urlPageNumber: number = (params.get('pageNumber') ?? pageNumber) as number;
    // const urlPageSize: number = (params.get('pageSize') ?? pageSize) as number;
    //
    // setPageNumber(urlPageNumber);
    // setPageSize(urlPageSize);

    axiosAuth.get<PaginatedList<WashingMachine>>(`washingMachines?pageSize=${pageSize}&pageNumber=${pageNumber}`)
      .then((result) => {
        setData(result.data);
      })
      .catch((error) => {
        setIsError(true);
      })
      .finally(() => {
        setIsLoading(false);
      });
  }, [pageNumber]);

  function changePage(addition: number) {
    if (data?.pageNumber! + addition < 1) {
      // console.warn('First page. Can\'t got any further.')
      return;
    }

    if (data?.pageNumber! + addition > data?.totalPages!) {
      // console.warn('Last page. Can\'t got any further.')
      return;
    }

    setPageNumber(pageNumber + addition);
  }

  return (
    <div className={styles.wrapper}>
      {isLoading ?
        <h1>Loading...</h1> :
        isError ?
          <h1>Error!</h1> :
          <>
            <div className={styles.cards}>
              {data?.items.map(
                (washingMachine) => {
                  return (
                    <>
                      <Card washingMachine={washingMachine} key={washingMachine.id} />
                      <Divider  />
                    </>
                  );
                })}
            </div>
            {data?.hasPreviousPage && <Link onClick={() => changePage(-1)} href={""}>previous page</Link>}
            <br />
            <Link href={""}>{data?.pageNumber}</Link>
            <br />
            {data?.hasNextPage && <Link onClick={() => changePage(1)} href={""}>next page</Link>}
            <br />
          </>
      }
    </div>
  );
}

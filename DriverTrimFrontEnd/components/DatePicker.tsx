import * as React from 'react';
import TextField from '@mui/material/TextField';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';

interface dateprops{
  name:string,
  val:any
}

export default function BasicDatePicker({name, val}:dateprops) {
  const [value, setValue] = React.useState<Date | null>(null);
console.log({value});
  return (
    <LocalizationProvider dateAdapter={AdapterDateFns}>
      <DatePicker
        label={name}
        value={value}
        onChange={(newValue) => {
          setValue(newValue);
          val(newValue)
        }}
        renderInput={(params) => <TextField {...params} />}
      />
    </LocalizationProvider>
  );
}
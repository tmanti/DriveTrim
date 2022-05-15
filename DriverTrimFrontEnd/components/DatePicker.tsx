import * as React from 'react';
import TextField from '@mui/material/TextField';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';

interface LabelName{
  name:string
}

export default function BasicDatePicker({name}:LabelName) {
  const [value, setValue] = React.useState<Date | null>(null);
console.log({value});
  return (
    <LocalizationProvider dateAdapter={AdapterDateFns}>
      <DatePicker
        label={name}
        value={value}
        onChange={(newValue) => {
          setValue(newValue);
        }}
        renderInput={(params) => <TextField {...params} />}
      />
    </LocalizationProvider>
  );
}
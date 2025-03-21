﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Models
{
    public class ResultViewModel
    {
        public ResultViewModel(bool isSucess = true, string message = "")
        {
            IsSucess = isSucess;
            Message = message;
        }

        public bool IsSucess { get; private set; }
        public string Message { get; private set; }

        public static ResultViewModel Sucess() => new ResultViewModel();
        public static ResultViewModel Error(string message) => new(false, message);
    }

    public class ResultViewModel<T> : ResultViewModel
    {
        public ResultViewModel(T? data, bool IsSucess = true, string message = "") : base(IsSucess, message)
        {
            Data = data;
        }

        public T? Data { get; private set; }


        //metodos de sucesso e error
        public static ResultViewModel<T> Sucess(T data) => new ResultViewModel<T>(data);

        public static ResultViewModel<T> Error(string message) => new (default, false, message);


    }
}


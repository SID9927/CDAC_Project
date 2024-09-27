import { useState } from "react";
import userServices from "../../Services/user.services";
import React from 'react';
import toast, { Toaster } from 'react-hot-toast';

function AddToCart(pid) {
    const addToCart = () => {
        let formdata = new FormData();
        formdata.append('qty', 1);
        userServices.addToCart(pid.productId, formdata)
            .then(response => {
                console.log("Added", response.data);
                toast.success('Product Added to Cart!',
                {
                    position: 'bottom-right',
                    style: {
                        borderRadius: '10px',
                        background: '#333',
                        color: '#fff',
                        height: '50px',
                        fontSize: '15px'
                    },
                });
            })
            .catch(error => {
                console.log("Something went wrong", error);
            });
    }

    return (
        <div>
        <button type="button" className="btn btn-sm add-to-cart-btn" onClick={addToCart}>Add To Cart</button>
        </div>
    );
}

export default AddToCart;
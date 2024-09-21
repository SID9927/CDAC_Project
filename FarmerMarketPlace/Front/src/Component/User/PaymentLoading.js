import Loader from "../Loader";

function PaymentLoading() {
  return (
    <div>
      <figure>
        <Loader />
        <figcaption className="text-center fw-bolder">
          You are being redirected to secure payment gateway....
        </figcaption>
      </figure>
    </div>
  );
}

export default PaymentLoading;
